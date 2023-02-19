import * as Router from 'react-router-dom';

import { useAuth } from './ProvideAuth';

function AuthorizedOnly() {
  const authType = useAuth();
  if (authType === 'guest') {
    return Router.redirect('/login');
  }
}

export default AuthorizedOnly;
