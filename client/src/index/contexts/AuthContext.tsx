import * as React from 'react';

import { UserDto } from '../api/api';

export interface AuthProps {
  user: UserDto | null;
  setAuthentication: (authentication: UserDto | null) => void;
  isAuthenticated: () => boolean;
}

/** Context that stores authentication information and allows changing it. */
const AuthContext = React.createContext<AuthProps | undefined>(undefined);

export default AuthContext;
