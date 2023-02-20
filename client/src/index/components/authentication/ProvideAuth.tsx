import * as React from 'react';

import AuthContext from '../../contexts/AuthContext';

interface Props {
  children: React.ReactNode;
}

function ProvideAuth({ children }: Props) {
  return <AuthContext.Provider value="guest">{children}</AuthContext.Provider>;
}

export default ProvideAuth;
