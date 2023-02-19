import * as React from 'react';

interface Props {
  children: React.ReactNode;
}

function Card({ children }: Props) {
  return <div className="card flex flex-col px-8 py-4">{children}</div>;
}
export default Card;
