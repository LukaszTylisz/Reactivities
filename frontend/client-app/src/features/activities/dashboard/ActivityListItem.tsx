import { Button, Item, Label } from "semantic-ui-react"
import { Activity } from "../../../models/activity"
import { Link } from "react-router-dom"
import { useStore } from "../../../app/stores/store";
import { useState, SyntheticEvent } from "react";

interface Props {
    activity: Activity
}

export function ActivityListItem({ activity }: Props) {
    const { activityStore } = useStore();
    const { deleteActivity, loading } = activityStore;

    const [target, setTarget] = useState('');

    function handleDeleteActivity(e: SyntheticEvent<HTMLButtonElement>, id: string) {
        setTarget(e.currentTarget.name);
        deleteActivity(id)
    }

    return (
        < Item key={activity.id} >
            <Item.Content>
                <Item.Header as='a'>{activity.title}</Item.Header>
                <Item.Meta>{activity.date}</Item.Meta>
                <Item.Description>
                    <div>{activity.description}</div>
                    <div>{activity.city}, {activity.venue}</div>
                </Item.Description>
                <Item.Extra>
                    <Button as={Link} to={`/activities/${activity.id}`} floated='right' content='View' color='blue' />
                    <Button loading={loading && target === activity.id}
                        name={activity.id} floated='right' content='Delete'
                        color='red'
                        onClick={(e) => handleDeleteActivity(e, activity.id)} />
                    <Label basic content={activity.category} />
                </Item.Extra>
            </Item.Content>
        </Item >
    )
} 