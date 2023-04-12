import * as React from 'react';

import AuthContext from '../contexts/AuthContext';

/** Shorthand custom hook for using the authentication context. */
export default function useAuth() {
  const context = React.useContext(AuthContext);
  if (context === undefined) {
    throw new Error('The useAuth hook must be used within an AuthProvider.');
  }

  return context;
}
