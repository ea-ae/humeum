import axios from 'axios';
import * as React from 'react';
import { useNavigate } from 'react-router-dom';

import useAuth from '../hooks/useAuth';
import { CategoryDto, TransactionCategoriesClient } from './api';

export default function fetchTransactionCategories(
  categories: CategoryDto[] | null,
  setCategories: (data: CategoryDto[]) => void
): [CategoryDto[] | null, (data: CategoryDto[]) => void] {
  const { user, setAuthentication } = useAuth();
  const navigate = useNavigate();

  React.useEffect(() => {
    const cancelSource = axios.CancelToken.source();

    if (categories === null) {
      if (user === null) {
        throw new Error('User was null in startup effect');
      }

      const client = new TransactionCategoriesClient();
      const userId = user.id.toString();
      const profileId = user.profiles[0].id;

      const get = () => client.getCategories(profileId, '1', userId);

      const set = (value: CategoryDto[]) => setCategories(value);

      const fail = () => {
        setAuthentication(null);
        navigate('/login');
      };

      TransactionCategoriesClient.callAuthenticatedEndpoint(get, set, fail, user.id);
    }

    return () => cancelSource.cancel();
  }, []);

  return [categories, setCategories];
}
