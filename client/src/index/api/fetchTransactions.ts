import axios from 'axios';
import * as React from 'react';
import { useNavigate } from 'react-router-dom';

import useAuth from '../hooks/useAuth';
import { TransactionDto, TransactionsClient } from './api';

export default function fetchTransactions(transactions: TransactionDto[] | null, setTransactions: (data: TransactionDto[]) => void) {
  const { user, setAuthentication } = useAuth();
  const navigate = useNavigate();

  React.useEffect(() => {
    const cancelSource = axios.CancelToken.source();

    if (transactions === null) {
      if (user === null) {
        throw new Error('User was null in startup effect');
      }

      const client = new TransactionsClient();
      const userId = user.id.toString();
      const profileId = user.profiles[0].id;

      const get = () => client.getTransactions(profileId, '1', userId, undefined, undefined, undefined, undefined, cancelSource.token);

      const set = (value: TransactionDto[]) => setTransactions(value);

      const fail = () => {
        setAuthentication(null);
        navigate('/login');
      };

      TransactionsClient.callAuthenticatedEndpoint(get, set, fail, user.id);
    }

    return () => cancelSource.cancel();
  }, []);

  return [transactions, setTransactions];
}
