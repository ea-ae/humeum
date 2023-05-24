import LogoutIcon from '@mui/icons-material/Logout';
import { useNavigate } from 'react-router-dom';

import useAuth from '../../hooks/useAuth';

export default function LogoutButton() {
  const { setAuthentication } = useAuth();
  const navigate = useNavigate();

  const onLogout = () => {
    setAuthentication(null);
    navigate('/login');
  };

  return <LogoutIcon onClick={onLogout} className="text-3xl cursor-pointer" />;
}
