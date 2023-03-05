import * as React from 'react';

import { UsersClient } from '../../api/api';
import AuthContext from '../../contexts/AuthContext';

const checkInitialAuthentication = async (): Promise<boolean> => {
  const client = new UsersClient();
  const authenticated = client
    .getCurrentUser()
    .then((res) => {
      if (res.status === 200) {
        // eslint-disable-next-line no-console
        console.log('Logged in');
      }
      return true;
    })
    .catch((err) => {
      // eslint-disable-next-line no-console
      console.log(err);
      return false;
    });

  return authenticated;
};

interface Props {
  children: React.ReactNode;
}

const { Provider } = AuthContext;

function ProvideAuth({ children }: Props) {
  const [isAuthenticated, setIsAuthenticated] = React.useState<boolean>(false);

  React.useEffect(() => {
    checkInitialAuthentication().then(setIsAuthenticated);
  }, []);

  const setAuthentication = (authenticated: boolean) => {
    setIsAuthenticated(authenticated);
  };

  return <Provider value={{ isAuthenticated, setAuthentication }}>{children}</Provider>;
}

export default ProvideAuth;
