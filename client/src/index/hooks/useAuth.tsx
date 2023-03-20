import * as React from 'react';

import AuthContext from '../contexts/AuthContext';

/** Shorthand custom hook for using the authentication context. */
const useAuth = () => React.useContext(AuthContext);

export default useAuth;
