import { useEffect, useState } from "react"
import type { Activity } from "./Interfaces/ActivitiesInterface"
import { List, ListItem, ListItemText, Typography } from "@mui/material"
import axios from "axios"


function App() {

  const [activities, setActivities] = useState<Activity[]>([])

  useEffect(() => {
    axios.get<Activity[]>('https://localhost:5001/api/activities')
     
      .then(response  => setActivities(response.data))
     

    return () => { }

  }, [])

  return (
    <>
      <Typography variant='h4' >Bienvenidos a My Social App</Typography>
      <List>
        {activities.map((Activity) => (
          <ListItem key={Activity.id}>
            <ListItemText>{Activity.title}</ListItemText>
          </ListItem>
        ))}

      </List>
    </>
  )
}

export default App
