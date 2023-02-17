import * as React from 'react';
import * as ReactRouter from 'react-router-dom';

const Welcome = (props: {username: string, savedUp: number, haveToSave: number, retireInYears: number}) => {
    return <div className="">
        <h1 className="mb-8 text-4xl">Welcome back, <b>{props.username}</b></h1>
        <h2 className="mb-4 text-2xl">You've saved up <b>{props.savedUp}</b> € so far</h2>
        <h2 className="mb-4 text-2xl">You still have to save <b>{props.haveToSave}</b> € this year</h2>
        <h2 className="mb-4 text-2xl">At this pace, you'll retire in <b>{props.retireInYears}</b> years</h2>
        <ReactRouter.Link to="/transactions">Go here</ReactRouter.Link>
    </div>;
}

export default Welcome;
