import axios from 'axios';
import * as React from 'react';
import { useNavigate } from 'react-router-dom';

import useAuth from '../hooks/useAuth';
import { ProfilesClient, ProjectionDto } from './api';

export default function fetchChart(
  chart: ProjectionDto | null,
  setChart: (data: ProjectionDto) => void
): [ProjectionDto | null, (data: ProjectionDto) => void] {
  const { user, setAuthentication } = useAuth();
  const navigate = useNavigate();

  React.useEffect(() => {
    const cancelSource = axios.CancelToken.source();

    if (chart === null) {
      if (user === null) {
        throw new Error('User was null in startup effect');
      }

      const client = new ProfilesClient();
      const userId = user.id.toString();
      const profileId = user.profiles[0].id;

      const get = () => client.generateChart(profileId, '1', userId, null, cancelSource.token);

      const set = (value: ProjectionDto) => setChart(value);

      const fail = () => {
        setAuthentication(null);
        navigate('/login');
      };

      ProfilesClient.callAuthenticatedEndpoint(get, set, fail, user.id);
    }

    return () => cancelSource.cancel();
  }, []);

  return [chart, setChart];
}
