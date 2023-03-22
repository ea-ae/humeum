import * as Mui from '@mui/material';
import * as React from 'react';
import * as Router from 'react-router-dom';

import { UsersClient } from '../../api/api';
import Card from '../../components/cards/Card';
import Layout from '../../components/layouts/Layout';
import useAuth from '../../hooks/useAuth';

export default function LoginPage() {
  const [user, setUser] = React.useState<string>('');
  const [password, setPassword] = React.useState<string>('');

  const authStatus = useAuth();
  const navigate = Router.useNavigate();

  React.useEffect(() => {
    if (authStatus.isAuthenticated()) {
      navigate('/');
    }
  }, [authStatus]);

  const login = () => {
    if (authStatus.isAuthenticated()) return; // already authenticated, skip API call

    const usersClient = new UsersClient();
    usersClient
      .signInUser(user, password, false)
      .then((res) => {
        // eslint-disable-next-line no-console
        console.log(res);
        authStatus.setAuthentication(res.result); // log in
      })
      .catch((err) => {
        // eslint-disable-next-line no-console
        console.log(err);
      });
  };

  return (
    <Layout centerFooter>
      <div className="flex justify-center">
        <Card padding={false} className="flex flex-col p-0">
          <div className="grow pt-5 pb-4 px-2 lg:px-12 text-2xl lg:text-3xl tracking-tighter font-semibold text-shadow text-center text-white bg-secondary-400 shadow-[0_3px_6px_-6px] shadow-black">
            Welcome to Humeum
          </div>
          <div className="flex flex-col px-8">
            <Mui.TextField
              className="min-w-[25vw] mt-8 mb-4"
              id="return"
              label="Username or email"
              variant="standard"
              value={user}
              onChange={(e) => setUser(e.target.value)}
            />
            <Mui.TextField
              className="min-w-[25vw] mb-4"
              id="return"
              label="Password"
              variant="standard"
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
            <Mui.Button className="my-8 px-8 py-3 text-lg tracking-widest" variant="contained" onClick={login}>
              Login
            </Mui.Button>
          </div>
        </Card>
      </div>
    </Layout>
  );
}
