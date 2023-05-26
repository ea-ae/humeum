import useAuth from '../../hooks/useAuth';
import NewsCard from './NewsCard';
import StatisticsCard from './StatisticsCard';

export default function HomePage() {
  const { user } = useAuth();

  if (user === null) {
    throw new Error('User was null in homepage');
  }

  return (
    <div className="flex flex-wrap justify-start items-start gap-4">
      <StatisticsCard />
      <NewsCard name={user.profiles[0].name} />
    </div>
  );
}
