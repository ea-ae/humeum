import * as React from 'react';
import Navigation from './Navigation';
import NavigationBar from './NavigationBar';

const Layout = (props: { children: React.ReactNode, sidebar: boolean }) => {
    return <div className="h-screen flex flex-col flex-grow bg-stone-50 bg-opacity-20">
        <NavigationBar />
        <div className="flex-grow flex flex-row">
            {props.sidebar ? <Navigation /> : <></>}
            <div className="flex-grow px-12 py-8">
                {props.children}
            </div>
        </div>
    </div>;
};

export default Layout;
