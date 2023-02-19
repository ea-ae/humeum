import Card from '../../shared/cards/Card';

interface Props {
  username: string;
  savedUp: number;
  haveToSave: number;
  retireInYears: number;
}

function HomeIndex({ username, savedUp, haveToSave, retireInYears }: Props) {
  return (
    <Card className="max-w-fit">
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
    </Card>
  );
}

export default HomeIndex;
