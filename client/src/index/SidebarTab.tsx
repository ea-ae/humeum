import * as Router from 'react-router-dom';

interface Props {
  label: string;
  path: string;
  activeLabel: string;
}

function SidebarTab({ label, path, activeLabel }: Props) {
  const extraClasses = activeLabel.toLowerCase() === label.toLowerCase() ? 'shadow-[inset_0_10px_15px_-15px] shadow-black bg-secondary-500' : '';

  return (
    <Router.Link to={path}>
      <div className={`px-7 py-3 text-gray-100 hover:text-white ${extraClasses}`}>{label.toUpperCase()}</div>
    </Router.Link>
  );
}

export default SidebarTab;
