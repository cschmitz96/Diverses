/** Minimal creature that blindly moves and attacks.*/

public class MyRover extends Creature {  
    String[] myPath = {};
    int temp = 0;
    
    @Override
    public void run() {
    	emitPheromone("here");  	
    	search();
    }
    
    public void search() {
    	if(searchOben()) {
    		search();
    	}
    	if(searchRechts()) {
    		search();
    	}
    	if(searchUnten()) {
    		search();
    	}
    	if(searchLinks()) {
    		search();
    	}
    	moveBack();
    }
    
    public boolean searchOben() {
    	turnLeft();
    	Observation[] myArray = observe();
    	if (!myArray[myArray.length -2].type.equals(Type.EMPTY)) {
    		attack();
    		turnRight();
    		return false;
    	}
    	else if (checkPheromone()) {
    		turnRight();
    		return false;
    	}
    	else {
    		moveForward();
    		emitPheromone("here");
    		myPath[temp]="O";
    		temp = temp+1;
    		turnRight();
    		return true;
    	}
    	
    }
    public boolean searchRechts() {
    	Observation[] myArray = observe();
    	if (!myArray[myArray.length -2].type.equals(Type.EMPTY)) {
    		attack();
    		return false;
    	}
    	else if (checkPheromone()) {
    		return false;
    	}
    	else {
    		moveForward();
    		emitPheromone("here");
    		myPath[temp]="R";
    		temp = temp+1;
    		return true;
    	}
    }
    public boolean searchUnten() {
    	turnRight();
    	Observation[] myArray = observe();
    	if (!myArray[myArray.length -2].type.equals(Type.EMPTY)) {
    		attack();
    		turnLeft();
    		return false;
    	}
    	else if (checkPheromone()) {
    		turnLeft();
    		return false;
    	}
    	else {
    		moveForward();
    		emitPheromone("here");
    		myPath[temp]="U";
    		temp = temp+1;
    		turnLeft();
    		return true;
    	}
    }
    public boolean searchLinks() {
    	turnLeft();
    	turnLeft();
    	Observation[] myArray = observe();
    	if (!myArray[myArray.length -2].type.equals(Type.EMPTY)) {
    		attack();
    		turnRight();
    		turnRight();
    		return false;
    	}
    	else if (checkPheromone()) {
    		turnRight();
    		turnRight();
    		return false;
    	}
    	else {
    		moveForward();
    		emitPheromone("here");
    		myPath[temp]="L";
    		temp = temp+1;
    		turnRight();
    		turnRight();
    		return true;
    	}
    }
    
    public void moveBack() {
    	if(myPath[temp-1]=="O") {
    		turnLeft();
    		moveBackward();
    		temp=temp-1;
    	}
    	if(myPath[temp-1]=="R") {
    		moveBackward();
    		temp=temp-1;
    	}
    	if(myPath[temp-1]=="U") {
    		turnRight();
    		moveBackward();
    		temp=temp-1;
    	}
    	if(myPath[temp-1]=="L") {
    		moveForward();
    		turnLeft();
    		turnLeft();
    		temp=temp-1;
    	}
    }
    
    public boolean checkPheromone() {
    	moveForward();
    	if(getPheromone() == "here") {
    		turnRight();
    		turnRight();
    		moveForward();
    		turnRight();
    		turnRight();
    		return true;
    	}
    	else {
    		turnRight();
    		turnRight();
    		moveForward();
    		turnRight();
    		turnRight();
    		return false;
    	}
    }

    @Override
	public String getAuthorName() {
        return "Chris";
    }

    @Override
	public String getDescription() {
        return "Own Robot";
    }
}
