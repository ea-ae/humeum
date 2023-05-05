import axios from 'axios';
import * as React from 'react';
import { useNavigate } from 'react-router-dom';

import { AssetDto, AssetsClient } from '../../api/api';
import NewItemCard from '../../components/cards/NewItemCard';
import useAuth from '../../hooks/useAuth';
import useCache from '../../hooks/useCache';
import AssetCard from './AssetCard';

export default function AssetList() {
  const [assets, setAssets] = useCache<AssetDto[] | null>('assets', null);
  const { user, setAuthentication } = useAuth();
  const navigate = useNavigate();

  const fail = () => {
    setAuthentication(null);
    navigate('/login');
  };

  const onAssetSave = (asset: AssetDto, realReturn: number, standardDeviation: number) => {
    const client = new AssetsClient();

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

  const onAssetDelete = (asset: AssetDto) => {
    const client = new AssetsClient();

    if (user === null || assets === null) {
      throw new Error('User or state was null in event handler');
    }

    const get = () => client.deleteAsset(user.profiles[0].id, asset.id, '1', user.id.toString());

    setAssets(assets.filter((a) => a.id !== asset.id));
    AssetsClient.callAuthenticatedEndpoint(get, null, fail, user.id);
  };

  React.useEffect(() => {
    const cancelSource = axios.CancelToken.source();

    if (user !== null && assets === null) {
      const client = new AssetsClient();

      const get = () => client.getAssets(user.profiles[0].id, '1', user.id.toString(), cancelSource.token);
      const set = (value: AssetDto[]) => setAssets(value);

      AssetsClient.callAuthenticatedEndpoint(get, set, fail, user.id, cancelSource.token);
    }

    return () => cancelSource.cancel();
  }, []);

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
      <NewItemCard text="Create custom asset" />
    </div>
  );
}
