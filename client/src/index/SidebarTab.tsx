import * as React from 'react';
import * as Router from 'react-router-dom';

interface Props {
  label: string;
  path: string;
  active: boolean;
}

function SidebarTab({ label, path, active }: Props) {
  const extraClasses = active
    ? 'shadow-[inset_0_10px_15px_-15px] shadow-black bg-secondary-500'
    : '';

  return (
    <Router.Link to={path}>
      <div className={`px-7 text-gray-100 hover:text-white ${extraClasses}`}>
        {label}
      </div>
    </Router.Link>
  );
}

export default SidebarTab;
