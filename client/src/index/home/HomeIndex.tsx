import * as Mui from '@mui/material';

interface Props {
  username: string;
  savedUp: number;
  haveToSave: number;
  retireInYears: number;
}

function HomeIndex({ username, savedUp, haveToSave, retireInYears }: Props) {
  return (
    <div className="card inline-block px-8 py-4">
      <h1 className="mb-4 text-xl">
        Welcome back, <b>{username}</b>
      </h1>
      <h2 className="mb-1">
        You have saved up <b>{savedUp}</b> € so far
      </h2>
      <h2 className="mb-1">
        You still have to save <b>{haveToSave}</b> € this year
      </h2>
      <h2 className="mb-1">
        At this pace, you will retire in <b>{retireInYears}</b> years
      </h2>
    </div>
  );
}

export default HomeIndex;
