import * as React from 'react';
import * as Router from 'react-router-dom';

interface Props {
  label: string;
  icon: React.ReactNode;
  path: string;
  activeLabel: string;
}

// text-gray-900 bg-primary-100
// shadow-[0_0_15px_-10px] shadow-black
// text-gray-100 bg-secondary-400

export default function SidebarTab({ label, icon, path, activeLabel }: Props) {
  // const extraClasses =
  //   activeLabel.toLowerCase() === label.toLowerCase() ? 'shadow-[inset_0_10px_15px_-15px] shadow-black bg-secondary-500' : '';

  const extraClasses = activeLabel.toLowerCase() === label.toLowerCase() ? 'text-gray-100 bg-secondary-400' : 'text-gray-900';

  return (
    <Router.Link to={path}>
      <div className={`px-5 py-3 ${extraClasses}`}>
        <span className="pr-2">{icon}</span>
        {label.toUpperCase()}
      </div>
    </Router.Link>
  );
}
