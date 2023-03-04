import * as React from 'react';

import AuthContext from '../../contexts/AuthContext';

interface Props {
  children: React.ReactNode;
}

const { Provider } = AuthContext;

function ProvideAuth({ children }: Props) {
  const [isAuthenticated, setIsAuthenticated] = React.useState<boolean>(false);

  const setAuthentication = (authenticated: boolean) => {
    setIsAuthenticated(authenticated);
  };

  return <Provider value={{ isAuthenticated, setAuthentication }}>{children}</Provider>;
}

export default ProvideAuth;
