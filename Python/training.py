from stable_baselines3.ppo import PPO
from stable_baselines3.common.env_util import make_vec_env
from UnityRLBridge.Python.RLBridge.bridge import Bridge
import environment
import numpy as np

bridge = Bridge("CubeAgent")

n = 0
def Make():
    global n,bridge
    env = environment.Environment(n,bridge)
    n += 1
    return env
        

vec_env = make_vec_env(Make,n_envs=2)

ppo = PPO(policy="MlpPolicy",env=vec_env,verbose=2,tensorboard_log="MoveToGoal")
ppo.learn(total_timesteps=100000,tb_log_name="Test")

ppo.save("MoveToGoal")

bridge.close_connection()