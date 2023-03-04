import DensitySmallIcon from '@mui/icons-material/DensitySmall';
import LogoutIcon from '@mui/icons-material/Logout';

interface Props {
  appLayout?: boolean;
}

function NavigationBar({ appLayout }: Props) {
  return (
    <div className="sticky top-0 z-10 flex flex-row justify-between drop-shadow-lg py-3 pl-5 pr-12 text-lg text-stone-50 bg-primary-500">
      <div className="flex items-center">
        {appLayout ? <DensitySmallIcon className="flex-shrink cursor-pointer" /> : null}
        <span className="ml-5 text-xl font-semibold cursor-default">Humeum</span>
      </div>
      {/* <div className="cursor-pointer text-stone-100 hover:text-white">Sign out</div> */}
      {appLayout ? <LogoutIcon className="text-3xl cursor-pointer" /> : null}
    </div>
  );
}

NavigationBar.defaultProps = { appLayout: true };

export default NavigationBar;
