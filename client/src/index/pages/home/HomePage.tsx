import Card from '../../components/cards/Card';
import useAuth from '../../hooks/useAuth';

export default function HomePage() {
  const { user } = useAuth();

  if (user === null) {
    throw new Error('User was null in homepage');
  }

  return (
    <div className="flex flex-wrap justify-start items-start gap-4">
      <Card className="max-h-[80vh] max-w-[40rem] overflow-auto">
        <h1 className="min-w-[25rem] mb-4 text-xl">
          Welcome back, <b>{user.profiles[0].name}</b>
        </h1>
        <p className="min-w-[25rem] mb-2">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut
          enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in
          reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt
          in culpa qui officia deserunt mollit anim id est laborum.
        </p>
        <p className="min-w-[25rem] mb-2">
          Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa
          quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas
          sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro
          quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt
          ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis
          suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit
          esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?
        </p>
      </Card>
      <Card className="max-w-[25rem] overflow-auto">
        <h1 className="min-w-[15rem] mb-4 text-xl">Statistics</h1>
        <p className="min-w-[15rem] mb-2">View your projected net worth based on your current transactions and assets.</p>
      </Card>
    </div>
  );
}
