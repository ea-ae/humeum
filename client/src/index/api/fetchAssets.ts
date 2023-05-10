import axios from 'axios';
import * as React from 'react';
import { useNavigate } from 'react-router-dom';

import useAuth from '../hooks/useAuth';
import { AssetDto, AssetsClient } from './api';

export default function fetchAssets(
  assets: AssetDto[] | null,
  setAssets: (data: AssetDto[]) => void
): [AssetDto[] | null, (data: AssetDto[]) => void] {
  const { user, setAuthentication } = useAuth();
  const navigate = useNavigate();

  React.useEffect(() => {
    const cancelSource = axios.CancelToken.source();

    if (assets === null) {
      if (user === null) {
        throw new Error('User was null in startup effect');
      }

      const client = new AssetsClient();
      const userId = user.id.toString();
      const profileId = user.profiles[0].id;

      const get = () => client.getAssets(profileId, '1', userId, cancelSource.token);

      const set = (value: AssetDto[]) => setAssets(value);

      const fail = () => {
        setAuthentication(null);
        navigate('/login');
      };

      AssetsClient.callAuthenticatedEndpoint(get, set, fail, user.id);
    }

    return () => cancelSource.cancel();
  }, []);

  return [assets, setAssets];
}
