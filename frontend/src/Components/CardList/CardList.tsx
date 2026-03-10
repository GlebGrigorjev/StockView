import React, { JSX } from 'react'
import Card from '../Card/Card'

interface Props {}

const CardList: React.FC<Props> = (props: Props): JSX.Element => {
  return (
    <div>
        <Card companyName="Apple" ticker="AAPL" price={110} />
        <Card companyName="Microsoft" ticker="MSFT" price={250} />
        <Card companyName="Google" ticker="GOOGL" price={150} />
    </div>
  )
}

export default CardList