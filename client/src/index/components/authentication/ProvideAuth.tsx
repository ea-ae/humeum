import * as React from 'react';

import { UserDto, UsersClient } from '../../api/api';
import AuthContext from '../../contexts/AuthContext';

/**
 * Checks whether the user is already authenticated through an HttpOnly cookie.
 * @returns Whether the user is authenticated.
 */
function isPreAuthenticated(): Promise<UserDto | null> {
  const client = new UsersClient();
  const authenticated = client
    .getCurrentUser()
    .then((res) => res.result)
    .catch((err) => {
      // eslint-disable-next-line no-console
      console.log(err);
      return null;
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
  const [user, setUser] = React.useState<UserDto | null>(null);

  React.useEffect(() => {
    isPreAuthenticated().then(setUser);
  }, []);

  const setAuthentication = (authentication: UserDto | null) => {
    setUser(authentication);
  };

  const isAuthenticated = () => user !== null;

  return <Provider value={{ user, setAuthentication, isAuthenticated }}>{children}</Provider>;
}
