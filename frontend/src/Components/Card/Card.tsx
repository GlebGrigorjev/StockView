import React, { JSX } from "react";
import "./Card.css";

interface Props {
  companyName: string;
  ticker: string;
  price: number;
}

const Card: React.FC<Props> = ({ companyName, ticker, price }: Props): JSX.Element => {
  return  (
  <div className="card">
    <img
        src="https://images.unsplash.com/photo-1506744038136-46273834b3fb?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8Y2FybHN8ZW58MHx8MHx8fDA%3D&auto=format&fit=crop&w=500&q=60"
        alt="Image"
    />
    <div className='details'>
      <h2>{companyName} ({ticker})</h2>
      <p>${price}</p>
    </div>
    <p className='info'>Additional information</p>
  </div>
  );
}

export default Card