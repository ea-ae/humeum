import * as React from 'react';

import { UserDto, UsersClient } from '../../api/api';
import AuthContext from '../../contexts/AuthContext';

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
    // isPreAuthenticated().then(setUser);
    const client = new UsersClient();

    const get = () => client.getCurrentUser('1');
    const set = (value: UserDto) => setUser(value);
    const fail = () => setUser(null);

    UsersClient.callAuthenticatedEndpoint(get, set, fail, -1);
  }, []);

  const setAuthentication = (authentication: UserDto | null) => {
    setUser(authentication);
  };

  const isAuthenticated = () => user !== null;

  return <Provider value={{ user, setAuthentication, isAuthenticated }}>{children}</Provider>;
}
