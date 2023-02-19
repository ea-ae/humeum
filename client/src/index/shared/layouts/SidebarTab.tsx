import * as React from 'react';
import * as Router from 'react-router-dom';

interface Props {
  label: string;
  icon: React.ReactNode;
  path: string;
  activeLabel: string;
}

function SidebarTab({ label, icon, path, activeLabel }: Props) {
  const extraClasses =
    activeLabel.toLowerCase() === label.toLowerCase()
      ? 'shadow-[inset_0_10px_15px_-15px] shadow-black bg-secondary-500'
      : '';

  return (
    <Router.Link to={path}>
      <div className={`px-5 py-3 text-gray-100 hover:text-white ${extraClasses}`}>
        <span className="pr-2">{icon}</span>
        {label.toUpperCase()}
      </div>
    </Router.Link>
  );
}

export default SidebarTab;
