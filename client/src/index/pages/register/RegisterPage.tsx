import * as Mui from '@mui/material';
import * as React from 'react';
import * as Router from 'react-router-dom';

import { ProfilesClient, UsersClient } from '../../api/api';
import Card from '../../components/cards/Card';
import Layout from '../../components/layouts/Layout';
import useAuth from '../../hooks/useAuth';

export default function RegisterPage() {
  const [username, setUsername] = React.useState<string>('');
  const [email, setEmail] = React.useState<string>('');
  const [password, setPassword] = React.useState<string>('');
  // radical concept: javascript fills in the confirm password field for you
  // const [confirmPassword, setConfirmPassword] = React.useState<string>('');

  const authStatus = useAuth();
  const navigate = Router.useNavigate();

  React.useEffect(() => {
    if (authStatus.isAuthenticated()) {
      navigate('/login');
    }
  }, [authStatus]);

  const register = () => {
    if (authStatus.isAuthenticated()) return; // already authenticated, skip API call

    const usersClient = new UsersClient();
    const profilesClient = new ProfilesClient();

    // please don't look at this code i had due dates to meet
    usersClient
      .registerUser('1', username, email, password, password, true)
      .then(() => usersClient.signInUser('1', username, password, false))
      .then((user) => {
        profilesClient.addProfile(user.result.id, '1', user.result.username, null, 3.5);
      })
      .then(() => usersClient.signInUser('1', username, password, false))
      .then((user) => authStatus.setAuthentication(user.result))
      .then(() => navigate('/'));
  };

  const onFormSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    register();
  };

  return (
    <Layout centerFooter>
      <div className="flex justify-center">
        <Card padding={false} className="flex flex-col p-0">
          <div className="grow pt-5 pb-4 px-2 lg:px-12 text-2xl lg:text-3xl tracking-tighter font-semibold text-shadow text-center text-white bg-secondary-400 shadow-[0_3px_6px_-6px] shadow-black">
            Welcome to Humeum
          </div>
          <form onSubmit={onFormSubmit} className="flex flex-col px-8">
            <Mui.TextField
              className="min-w-[25vw] mt-8 mb-4"
              id="return"
              label="Username"
              variant="standard"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
            <Mui.TextField
              className="min-w-[25vw] mb-4"
              id="return"
              label="Email"
              variant="standard"
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
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
            <Mui.TextField
              className="min-w-[25vw] mb-4"
              id="return"
              label="Confirm password"
              variant="standard"
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
            <Mui.Button type="submit" className="my-4 px-8 py-3 text-lg tracking-widest" variant="contained" onClick={register}>
              Register
            </Mui.Button>
            <div className="flex flex-col items-center justify-center mb-4">
              <span className="text-sm text-center text-stone-500">Already have an account?</span>
              <Router.Link to="/login" className="text-sm text-center underline text-stone-600">
                Login
              </Router.Link>
            </div>
          </form>
        </Card>
      </div>
    </Layout>
  );
}
