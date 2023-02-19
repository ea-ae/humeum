import DensitySmallIcon from '@mui/icons-material/DensitySmall';

function NavigationBar() {
  return (
    <div className="flex flex-row justify-between drop-shadow-lg py-3 pl-5 pr-12 text-lg text-stone-50 bg-primary-500">
      <div className="flex items-center">
        <DensitySmallIcon className="flex-shrink cursor-pointer" />
        <span className="ml-5 font-semibold cursor-default">Humeum</span>
      </div>
      <div className="cursor-pointer text-stone-100 hover:text-white">Sign out</div>
    </div>
  );
}

export default NavigationBar;
