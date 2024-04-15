from UnityRLBridge.Python.RLBridge.bridge import Bridge
import gym
import numpy as np
import math

class Environment(gym.Env):

    def __init__(self,index,bridge):
        self.bridge = bridge
        self.index = index
        self.action_space = gym.spaces.Box(low=-1.0,high=1.0,shape=(self.bridge.continuous_action_size,))
        self.observation_space = gym.spaces.Box(low=-math.inf,high=math.inf,shape=(self.bridge.observation_size,))

    def reset(self):
        state = self.bridge.reset(self.bridge.agent_names[self.index])
        return np.array(state[0])
    
    def step(self,action):
        state = self.bridge.send_action(self.bridge.agent_names[self.index],list(action.astype(np.float64)),[])
        return (np.array(state[0]),state[1],state[2],{})
    
    def seed(self, seed=None):
        self.np_random, seed = gym.utils.seeding.np_random(seed)
        return [seed]
