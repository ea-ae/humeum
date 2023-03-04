import * as Mui from '@mui/material';
import * as React from 'react';
import * as Router from 'react-router-dom';

import Card from '../../components/cards/Card';
import Layout from '../../components/layouts/Layout';
import useAuth from '../../hooks/useAuth';

function LoginIndex() {
  const [email, setEmail] = React.useState<string>('');
  const [password, setPassword] = React.useState<string>('');
  const authStatus = useAuth();
  const navigate = Router.useNavigate();

  const login = () => {
    authStatus.setAuthentication(true); // log in
    navigate('/');
  };

  return (
    <Layout>
      <div className="flex flex-row justify-center mt-6">
        <Card className="flex flex-col">
          <h1 className="mt-5 mb-4 text-3xl tracking-tighter font-semibold text-center">Welcome to Humeum</h1>
          <Mui.TextField
            className="min-w-[25vw] my-4"
            id="return"
            label="Email address"
            variant="standard"
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
          <Mui.TextField
            className="min-w-[25vw] my-4"
            id="return"
            label="Password"
            variant="standard"
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
          <Mui.Button className="mt-10 mb-3 px-8 py-3 text-lg tracking-widest" variant="contained" onClick={login}>
            Login
          </Mui.Button>
        </Card>
      </div>
    </Layout>
  );
}

export default LoginIndex;
