import * as React from 'react';
import * as Router from 'react-router-dom';

const AuthContext = React.createContext('guest');

interface Props {
  children: React.ReactNode;
}

export function ProvideAuth({ children }: Props) {
  return <AuthContext.Provider value="guest">{children}</AuthContext.Provider>;
}

export const useAuth = () => React.useContext(AuthContext);

export const authorize = () => {
  const authType = useAuth();
  if (authType === 'guest') {
    return Router.redirect('/login');
  }
  return null;
};
