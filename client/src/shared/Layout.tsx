import * as React from 'react';
import NavigationBar from './NavigationBar';


const Layout = (props: { children: React.ReactNode, sidebar?: React.ReactNode }) => {
    return <div className="h-screen flex flex-col flex-grow bg-stone-50 bg-opacity-20">
        <NavigationBar />
        <div className="flex-grow flex flex-row">
            {props.sidebar === undefined ? null : props.sidebar}
            <div className="flex-grow px-12 py-8">
                {props.children}
            </div>
        </div>
    </div>;
};

export default Layout;
