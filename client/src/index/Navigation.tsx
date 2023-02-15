import * as React from 'react';
import * as Mui from '@mui/material';


const Navigation = () => {
    const [navSelection, setNavSelection] = React.useState(0);

    const handleNavigationChange = (event: React.SyntheticEvent, newValue: number) => {
        setNavSelection(newValue);
    };

    return (
        <Mui.Box className="shadow-inner bg-primary-400">
            <Mui.Tabs TabIndicatorProps={{className: 'bg-primary-200 w-0'}}
                      
                      value={navSelection} 
                      onChange={handleNavigationChange} 
                      orientation="vertical"
                      variant="scrollable" 
                      scrollButtons="auto"
            >
                <Mui.Tab className="px-7 text-gray-100 hover:text-white"
                         classes={{ selected: 'shadow-[inset_0_10px_15px_-15px] shadow-black bg-primary-500' }} 
                         label="Home" />
                <Mui.Tab className="px-7 text-gray-100 hover:text-white"
                         classes={{ selected: 'shadow-[inset_0_10px_15px_-15px] shadow-black bg-primary-500' }} 
                         label="Transactions" />
                <Mui.Tab className="px-7 text-gray-100 hover:text-white"
                         classes={{ selected: 'shadow-[inset_0_10px_15px_-15px] shadow-black bg-primary-500' }} 
                         label="Assets" />
                <Mui.Tab className="px-7 text-gray-100 hover:text-white"
                         classes={{ selected: 'shadow-[inset_0_10px_15px_-15px] shadow-black bg-primary-500' }} 
                         label="Taxes" />
                <Mui.Tab className="px-7 text-gray-100 hover:text-white"
                         classes={{ selected: 'shadow-[inset_0_10px_15px_-15px] shadow-black bg-primary-500' }} 
                         label="Settings" />
            </Mui.Tabs>
        </Mui.Box>
    );
};

export default Navigation;
