import * as React from 'react';

interface Props {
  children: React.ReactNode;
  className?: string;
}

function Card({ children, className }: Props) {
  return <div className={`card flex flex-col px-8 py-4 ${className}`}>{children}</div>;
}

Card.defaultProps = {
  className: '',
};

export default Card;
