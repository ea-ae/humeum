import * as React from 'react';

import { UsersClient } from '../api/api';

export interface AuthProps {
  isAuthenticated: boolean;
  setAuthentication: (authenticated: boolean) => void;
}

const client = new UsersClient();

const AuthContext = React.createContext<AuthProps>({
  isAuthenticated: false,
  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  setAuthentication: (_authentication: boolean) => {
    // do nothing
  },
});

export default AuthContext;
