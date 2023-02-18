interface Props {
  username: string;
  savedUp: number;
  haveToSave: number;
  retireInYears: number;
}

function HomeIndex({ username, savedUp, haveToSave, retireInYears }: Props) {
  return (
    <div>
      <h1 className="mb-8 text-4xl">
        Welcome back, <b>{username}</b>
      </h1>
      <h2 className="mb-4 text-2xl">
        You have saved up <b>{savedUp}</b> € so far
      </h2>
      <h2 className="mb-4 text-2xl">
        You still have to save <b>{haveToSave}</b> € this year
      </h2>
      <h2 className="mb-4 text-2xl">
        At this pace, you will retire in <b>{retireInYears}</b> years
      </h2>
    </div>
  );
}

export default HomeIndex;
