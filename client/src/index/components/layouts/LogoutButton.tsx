import LogoutIcon from '@mui/icons-material/Logout';
import { useNavigate } from 'react-router-dom';

import { UsersClient } from '../../api/api';
import useAuth from '../../hooks/useAuth';

export default function LogoutButton() {
  const { setAuthentication } = useAuth();
  const navigate = useNavigate();

  const onLogout = async () => {
    const client = new UsersClient();

    setAuthentication(null);
    await client.signOutUser('1', null);
    navigate('/login');
  };

  return <LogoutIcon onClick={onLogout} className="text-3xl cursor-pointer" />;
}
