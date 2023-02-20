import * as React from 'react';
import * as Router from 'react-router-dom';

import useAuth from '../../hooks/useAuth';

interface Props {
  children: React.ReactElement;
}

function Authenticated({ children }: Props) {
  const authenticated = useAuth() !== 'guest';

  return authenticated ? children : <Router.Navigate to="/login" replace />;
}

export default Authenticated;
