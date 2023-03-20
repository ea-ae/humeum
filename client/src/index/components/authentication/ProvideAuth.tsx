import * as React from 'react';

import { UsersClient } from '../../api/api';
import AuthContext from '../../contexts/AuthContext';

/**
 * Checks whether the user is already authenticated through an HttpOnly cookie.
 * @returns Whether the user is authenticated.
 */
function isPreAuthenticated(): Promise<boolean> {
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
}

const { Provider } = AuthContext;

interface Props {
  children: React.ReactNode;
}

/**
 * Component that provides authentication information to the rest of the application.
 * @param props.children Components that need authentication information.
 * @returns Provider component.
 */
export default function ProvideAuth({ children }: Props) {
  const [isAuthenticated, setIsAuthenticated] = React.useState<boolean>(false);

  React.useEffect(() => {
    isPreAuthenticated().then(setIsAuthenticated);
  }, []);

  const setAuthentication = (authenticated: boolean) => {
    setIsAuthenticated(authenticated);
  };

  return <Provider value={{ isAuthenticated, setAuthentication }}>{children}</Provider>;
}
