import React, { Fragment } from 'react';
//import { Tab } from '@headlessui/react';
import { Box, Tabs, Tab } from '@mui/material';


const NavigationBar = () => {
    return (
        <div className="flex flex-row justify-between drop-shadow-md py-4 px-10 text-lg text-stone-50 bg-primary-400">
            <span className="font-semibold">
                Humeum
            </span>
            <div className="cursor-pointer text-stone-100 hover:text-white">
                Sign out
            </div>
        </div>
    );
};

export default NavigationBar;