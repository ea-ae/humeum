import DensitySmallIcon from '@mui/icons-material/DensitySmall';

import LogoutButton from './LogoutButton';

interface Props {
  appLayout?: boolean;
}

export default function NavigationBar({ appLayout }: Props) {
  return (
    <div className="flex flex-row justify-between drop-shadow-lg py-3 pl-5 pr-12 text-lg tracking-wide text-stone-50 bg-primary-500">
      <div className="flex items-center">
        {appLayout ? <DensitySmallIcon className="flex-shrink cursor-pointer" /> : null}
        <span className="ml-5 text-xl font-semibold cursor-default">Humeum</span>
      </div>
      {appLayout ? <LogoutButton /> : null}
    </div>
  );
}

NavigationBar.defaultProps = { appLayout: true };
