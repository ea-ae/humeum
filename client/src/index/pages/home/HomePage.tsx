import Card from '../../components/cards/Card';
import useAuth from '../../hooks/useAuth';

export default function HomePage() {
  const { user } = useAuth();

  if (user === null) {
    throw new Error('User was null in homepage');
  }

  return (
    <Card className="max-w-fit">
      <h1 className="mb-4 text-xl">
        Welcome back, <b>{user.profiles[0].name}</b>
      </h1>
      <h2 className="mb-1">
        You have saved up <b>144 200</b> € so far
      </h2>
      <h2 className="mb-1">
        You still have to save <b>9 200</b> € this year
      </h2>
      <h2 className="mb-1">
        At this pace, you will retire in <b>11</b> years
      </h2>
    </Card>
  );
}
