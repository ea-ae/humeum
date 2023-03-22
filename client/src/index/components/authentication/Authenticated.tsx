import * as React from 'react';
import * as Router from 'react-router-dom';

import useAuth from '../../hooks/useAuth';

interface Props {
  children: React.ReactElement;
}

// todo we should rename the HOC to withAuthenticated?

/**
 * Component that conditionally renders either its children or redirects to the login page.
 * @param props.children Children to render if authenticated.
 * @returns Children or login redirect.
 */
export default function Authenticated({ children }: Props) {
  const { isAuthenticated } = useAuth();

  return isAuthenticated() ? children : <Router.Navigate to="/login" replace />;
}
