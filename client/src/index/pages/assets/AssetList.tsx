import * as React from 'react';
import { useNavigate } from 'react-router-dom';

import { AssetDto, AssetsClient } from '../../api/api';
import fetchAssets from '../../api/fetchAssets';
import NewItemCard from '../../components/cards/NewItemCard';
import useAuth from '../../hooks/useAuth';
import useCache from '../../hooks/useCache';
import AssetCard from './AssetCard';
import CreateDialog from './CreateDialog';

export default function AssetList() {
  const [assets, setAssets] = fetchAssets(...useCache<AssetDto[] | null>('assets', null));
  const [isCreateDialogOpen, setIsCreateDialogOpen] = React.useState<boolean>(false);

  const { user, setAuthentication } = useAuth();
  const navigate = useNavigate();

  const client = new AssetsClient();

  const fail = () => {
    setAuthentication(null);
    navigate('/login');
  };

  const onAssetSave = (asset: AssetDto, realReturn: number, standardDeviation: number) => {
    if (user === null || assets === null) {
      throw new Error('User or state was null in event handler');
    }

    const get = () =>
      client.replaceAsset(
        user.profiles[0].id,
        asset.id,
        '1',
        user.id.toString(),
        asset.name,
        asset.description,
        realReturn,
        standardDeviation,
        asset.type.code
      );

    const set = () => {
      const newAsset = asset;
      newAsset.returnRate = realReturn;
      setAssets(assets.map((a) => (a.id === asset.id ? newAsset : a)));
    };

    AssetsClient.callAuthenticatedEndpoint(get, set, fail, user.id);
  };

  const onAssetCreate = (createdAsset: AssetDto) => {
    const asset = createdAsset;

    if (user === null || assets === null) {
      throw new Error('User or state was null in event handler');
    }

    const get = async () => {
      const result = await client.addAsset(
        user.id,
        user.profiles[0].id,
        '1',
        asset.name,
        asset.description,
        asset.returnRate,
        asset.standardDeviation,
        asset.type.code
      );

      const createdId = parseInt(result.headers.location.split('/').pop() as string, 10);
      asset.id = createdId;

      return result;
    };

    const set = () => setAssets([...assets, asset]);

    AssetsClient.callAuthenticatedEndpoint(get, set, fail, user.id);
  };

  const onAssetDelete = (asset: AssetDto) => {
    if (user === null || assets === null) {
      throw new Error('User or state was null in event handler');
    }

    const get = () => client.deleteAsset(user.profiles[0].id, asset.id, '1', user.id.toString());

    setAssets(assets.filter((a) => a.id !== asset.id));
    AssetsClient.callAuthenticatedEndpoint(get, null, fail, user.id);
  };

  if (assets === null) return <p>Loading...</p>;

  return (
    <div className="grid grid-cols-1 gap-4 lg:grid-cols-2 2xl:grid-cols-3">
      {assets.map((asset) => (
        <AssetCard
          key={asset.id}
          name={asset.name}
          description={asset.description ?? ''}
          returnRate={asset.returnRate}
          standardDeviation={asset.standardDeviation}
          readOnly={asset.default}
          onSave={(realReturn: number, standardDeviation: number) => onAssetSave(asset, realReturn, standardDeviation)}
          onDelete={() => onAssetDelete(asset)}
        />
      ))}
      <NewItemCard text="Create custom asset" onClick={() => setIsCreateDialogOpen(true)} />
      <CreateDialog isOpen={isCreateDialogOpen} onClose={() => setIsCreateDialogOpen(false)} onCreate={onAssetCreate} />
    </div>
  );
}
