import * as React from 'react';
import * as Router from 'react-router-dom';

import useAuth from '../../hooks/useAuth';

interface Props {
  children: React.ReactElement;
}

function Authenticated({ children }: Props) {
  const authStatus = useAuth();

  return authStatus.isAuthenticated ? children : <Router.Navigate to="/login" replace />;
}

export default Authenticated;
