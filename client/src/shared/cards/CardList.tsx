import * as React from 'react';

interface Props {
  children: React.ReactNode;
}

function CardList({ children }: Props) {
  return <div className="grid grid-cols-1 gap-4 lg:grid-cols-2 2xl:grid-cols-3">{children}</div>;
}

export default CardList;
