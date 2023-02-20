import * as React from 'react';

import AuthContext from '../contexts/AuthContext';

const useAuth = () => React.useContext(AuthContext);

export default useAuth;
