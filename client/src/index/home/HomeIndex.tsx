import * as React from 'react';
import * as Router from 'react-router-dom';


const HomeIndex = (props: {username: string, savedUp: number, haveToSave: number, retireInYears: number}) => {
    return <div className="">
        <h1 className="mb-8 text-4xl">Welcome back, <b>{props.username}</b></h1>
        <h2 className="mb-4 text-2xl">You have saved up <b>{props.savedUp}</b> € so far</h2>
        <h2 className="mb-4 text-2xl">You still have to save <b>{props.haveToSave}</b> € this year</h2>
        <h2 className="mb-4 text-2xl">At this pace, you will retire in <b>{props.retireInYears}</b> years</h2>
        <Router.Link to="/transactions">Go here</Router.Link>
    </div>;
};

export default HomeIndex;
