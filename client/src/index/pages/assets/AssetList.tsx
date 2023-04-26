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

  const onAssetSave = (id: number, realReturn: number, standardDeviation: number) => {
    console.log(`saving ${id} with ${realReturn} and ${standardDeviation}`);
  };

  React.useEffect(() => {
    const cancelSource = axios.CancelToken.source();

    if (user !== null && assets === null) {
      const client = new AssetsClient();

      const get = () => client.getAssets(user.profiles[0].id, user.id.toString(), cancelSource.token);

      const set = (value: AssetDto[]) => setAssets(value);

      const fail = () => {
        setAuthentication(null);
        navigate('/login');
      };

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
          onSave={(realReturn: number, standardDeviation: number) => onAssetSave(asset.id, realReturn, standardDeviation)}
        />
      ))}
      <NewItemCard text="Create custom asset" />
    </div>
  );

  // return (
  //   <div className="grid grid-cols-1 gap-4 lg:grid-cols-2 2xl:grid-cols-3">
  //     <AssetCard
  //       name="Index fund (default)"
  //       description="Index funds track the performance of a particular market index; great diversification, low fees, and easy management."
  //       returnRate={8.1}
  //       standardDeviation={15.2}
  //       readOnly
  //     />
  //     <AssetCard
  //       name="Bond fund (default)"
  //       description="Bonds funds provide great diversification potential and are generally less volatile than other securities (depending on bond type)."
  //       returnRate={1.9}
  //       standardDeviation={3}
  //       readOnly
  //     />
  //     <AssetCard name="Custom asset type 1" description="Custom asset description goes here." returnRate={5} standardDeviation={5} />
  //     <AssetCard
  //       name="Custom asset type 2"
  //       description={
  //         'Custom asset description goes here. These descriptions can sometimes get very long, ' +
  //         'inwhichcasewordwrappingandgridlayoutsshouldtakecareofitwellenough.'
  //       }
  //       returnRate={9.4}
  //       standardDeviation={44.21}
  //     />
  //     <AssetCard name="Custom asset type 3" description="Custom asset description goes here." returnRate={1} standardDeviation={1} />
  //     <NewItemCard text="Create custom asset" />
  //   </div>
  // );
}
