import * as React from 'react';
import * as Router from 'react-router-dom';


const SidebarTab = (props: {label: string, path: string, active: boolean}) => {
    const extraClasses = props.active ? 'shadow-[inset_0_10px_15px_-15px] shadow-black bg-secondary-500' : '';

    return <Router.Link to={props.path}>
        <div className={`px-7 text-gray-100 hover:text-white ${extraClasses}`}>
            {props.label}
        </div>
    </Router.Link>;
};

export default SidebarTab;
