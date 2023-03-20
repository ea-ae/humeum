import * as React from 'react';

interface AuthProps {
  isAuthenticated: boolean;
  setAuthentication: (authenticated: boolean) => void;
}

/** Context that stores authentication information and allows changing it. */
const AuthContext = React.createContext<AuthProps>({
  isAuthenticated: false,
  setAuthentication: (_authentication: boolean) => {
    // do nothing
  },
});

export default AuthContext;
